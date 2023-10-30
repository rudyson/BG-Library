const PROXY_CONFIG = [
  {
    context: [
      "/api/auth/",
    ],
    target: "https://localhost:44301",
    secure: false
  },
  {
    context: [
      "/api/data/"
    ],
    target: "https://localhost:44303",
    "secure": false,
    "changeOrigin": true,
    "pathRewrite": {"^/api" : ""}
  }
]

module.exports = PROXY_CONFIG;
