const PROXY_CONFIG = [
  {
    "/apiid":{
      target: "http://localhost:44301",
      secure: false
    }
  },
  {
    "/apidt":{
      target: "http://localhost:44303",
      secure: false
    }
  }
]

module.exports = PROXY_CONFIG;
