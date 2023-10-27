const PROXY_CONFIG = [
  {
    context: [
      "/auth",
    ],
    target: "http://localhost:9010",
    secure: false
  },
  {
    context: [
      "/api/books",
      "/api/authors",
    ],
    target: "http://localhost:9012",
    "secure": false,
    "changeOrigin": true,
    "pathRewrite": {"^/api" : ""}
  }
]

module.exports = PROXY_CONFIG;
