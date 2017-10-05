﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebServer.Server.Common;
using WebServer.Server.Enums;
using WebServer.Server.Exceptions;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.HTTP
{
    public class HttpRequest : IHttpRequest
    {

        public HttpRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            this.FormData = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();

            this.ParseRequest(requestText);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod Method { get; private set; }

        public string URL { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestText)
        {
            var requestLines = requestText.Split(Environment.NewLine);

            if (!requestLines.Any())
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
            var requestLine = requestLines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.Method = this.ParseMethod(requestLine.First());
            this.URL = requestLine[1];
            this.Path = this.ParsePath(this.URL);
            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }

        private void ParseFormData(string formDataLine)
        {
            if (this.Method  == HttpRequestMethod.GET)
            {
                return;
            }

            // username=pesho&pass=123
            this.ParseQuery(formDataLine, this.QueryParameters);
        }

        private void ParseParameters()
        {
            if (!this.URL.Contains('?'))
            {
                return;
            }

            var query = this.URL.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries).Last();

            // /register?name=ivan
            this.ParseQuery(query, this.UrlParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {

            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dict.Add(queryKey, queryValue);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            var emptyLineAfterHeaderIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < emptyLineAfterHeaderIndex; i++)
            {
                var currentLine = requestLines[i];
                var headerParts = currentLine.Split(new[] { ": " },StringSplitOptions.RemoveEmptyEntries);

                if (headerParts.Length != 2)
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }

                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerKey, headerValue);
                this.Headers.Add(header);
            }

            if (!Headers.ContainsKey("Host"))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
        }

        private string ParsePath(string url)
        {
            return url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private HttpRequestMethod ParseMethod(string method)
        {

                HttpRequestMethod parsedMethod;
                if (!Enum.TryParse<HttpRequestMethod>(method, true, out parsedMethod))
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }

            return parsedMethod;
        }
    }
}