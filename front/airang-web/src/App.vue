<template>
  <div id="contents">
    <a @click="top" class="top">
      <img src="./assets/top.png" alt="top" />
    </a>
    <div class="page1">
      <div class="left-box">
        <div class="logo">
          <img src="./assets/logo.png" alt="logo" />
        </div>
        <div class="page1-img">
          <img src="./assets/main-img.png" alt="main-img" />
        </div>
      </div>
      <div class="right-box">
        <a href="" download class="download">
          <img src="./assets/download.png" alt="download" />
        </a>
        <div class="text">
          아이랑과 함께<br />
          <span>생생한 동화</span>를 만나보세요!
        </div>
      </div>
    </div>
    <div class="page2">
      <h1 data-aos="fade-up" data-aos-delay="100" data-aos-duration="1000">
        <div class="img-box">
          <img src="./assets/arang.png" alt="arang" />
        </div>
        <div class="img-box">
          <img src="./assets/number1.png" alt="number1" />
        </div>
        <span>200여 종류</span>의 다양한 <span>&nbsp;동화</span>들!
      </h1>
      <div
        class="page2-bg"
        data-aos="fade-up"
        data-aos-duration="2000"
        data-aos-delay="110"
      >
        <img src="./assets/page2-bg.png" alt="page2 img" />
      </div>
    </div>
    <div class="page3">
      <h1 data-aos="fade-up" data-aos-delay="120" data-aos-duration="1000">
        <div class="img-box">
          <img src="./assets/arang.png" alt="arang" />
        </div>
        <div class="img-box">
          <img src="./assets/number2.png" alt="number1" />
        </div>
        아이들의 &nbsp; <span>독서 흥미</span> &nbsp;및&nbsp;
        <span>창의력 증진!</span>
      </h1>
      <div
        class="ar-img"
        data-aos="fade-up"
        data-aos-delay="130"
        data-aos-duration="1000"
      >
        <img src="./assets/ar.gif" alt="ar" />
        <img src="./assets/page3-child.png" alt="child" />
      </div>
    </div>
    <div class="page4">
      <h1 data-aos="fade-up" data-aos-delay="140" data-aos-duration="1000">
        <div class="img-box">
          <img src="./assets/arang.png" alt="arang" />
        </div>
        <div class="img-box">
          <img src="./assets/number3.png" alt="number1" />
        </div>
        <span>다양한 목소리</span>로 동화 몰입도 향상!
      </h1>
      <div
        class="narr-img"
        data-aos="fade-up"
        data-aos-delay="150"
        data-aos-duration="1000"
      >
        <img src="./assets/narr.png" alt="narr" />
      </div>
    </div>
  </div>
</template>

<script>
import AOS from "aos";
import "aos/dist/aos.css";
export default {
  name: "App",
  setup() {
    AOS.init();

    const top = () => {
      window.scrollTo({ top: 0, behavior: "smooth" });
    };

    window.onbeforeunload = () => {
      window.scrollTo(0, 0);
    };

    return { top };
  },
};
</script>

<style lang="scss">
@font-face {
  font-family: "ONE-Mobile-POP";
  src: url("https://cdn.jsdelivr.net/gh/projectnoonnu/noonfonts_2105_2@1.0/ONE-Mobile-POP.woff")
    format("woff");
  font-weight: normal;
  font-style: normal;
}
@font-face {
  font-family: "ONE-Mobile-Regular";
  src: url("https://cdn.jsdelivr.net/gh/projectnoonnu/noonfonts_2105_2@1.0/ONE-Mobile-Regular.woff")
    format("woff");
  font-weight: normal;
  font-style: normal;
}

html {
  width: 100%;
  height: 100%;
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
  text-decoration: none;
  list-style: none;
  font-family: "ONE-Mobile-Regular";
}

body {
  height: 100%;
  width: 100%;
  margin: 0;
  padding: 0;
  font-size: 16px;
  background-color: #fff1e6;
  overflow: auto;
  color: #414141;
  transition: 0.3s ease;
}

body::-webkit-scrollbar {
  width: 10px;
}
body::-webkit-scrollbar-thumb {
  background-color: #825a3d;
  border-radius: 10px;
  background-clip: padding-box;
  border: 2px solid transparent;
}
body::-webkit-scrollbar-track {
  background-color: #c29876;
  border-radius: 10px;
  box-shadow: inset 0px 0px 5px white;
}

.top {
  display: block;
  width: 129px;
  height: 180px;
  position: fixed;
  right: 5%;
  bottom: 6%;
  transition: 0.7s ease;
  cursor: pointer;
  z-index: 100;

  img {
    width: 100%;
    height: 100%;
  }
}

.page1 {
  display: flex;
  background: url("./assets/main-bg.png");
  background-size: contain;
  background-repeat: round;
  height: 100vh;

  .left-box {
    width: 70%;
    display: flex;
    flex-direction: column;
    padding-left: 3%;

    .logo {
      height: 25%;
      width: 100%;
      display: flex;
      align-items: center;

      img {
        margin: 5% 0 0 5%;
        display: block;
        width: 150px;
      }
    }

    .page1-img {
      width: 100%;
      height: 75%;
      display: flex;
      align-items: flex-end;
      padding-bottom: 5%;

      img {
        display: block;
        width: 72%;
        margin-left: 8%;
      }
    }
  }
  .right-box {
    width: 30%;
    display: flex;
    flex-direction: column;

    .download {
      width: 100%;
      display: flex;
      align-items: flex-end;
      height: 30%;
      img {
        width: 60%;
      }
    }
    .text {
      height: 70%;
      padding-top: 5%;
      padding-left: 4%;
      text-shadow: 0 0 1px #444444;
      font-size: 28px;

      span {
        font-family: "ONE-Mobile-POP";
        color: #63432c;
      }
    }
  }
}

.page2 {
  padding: 7% 0;
  h1 {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 3%;

    .img-box {
      width: 100px;
      height: 100px;
      display: flex;
      justify-content: center;
      align-items: center;

      &:first-of-type {
        align-items: flex-end;
        img {
          width: 95px;
          height: 99px;
        }
      }

      &:last-of-type {
        img {
          width: 88px;
          height: 62px;
        }
      }
    }
    span {
      font-family: "ONE-Mobile-POP";
      font-size: 43px;

      &:last-of-type {
        color: #63432c;
      }
    }
  }
  .page2-bg {
    width: 67%;
    margin: auto;
    img {
      width: 100%;
    }
  }
}

.page3 {
  padding: 7% 0 8%;
  height: auto;
  background-color: #ffe9d8;
  h1 {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 3%;

    .img-box {
      width: 100px;
      height: 100px;
      display: flex;
      justify-content: center;
      align-items: center;

      &:first-of-type {
        align-items: flex-end;
        img {
          width: 95px;
          height: 99px;
        }
      }

      &:last-of-type {
        img {
          width: 88px;
          height: 62px;
        }
      }
    }
    span {
      font-family: "ONE-Mobile-POP";
      font-size: 43px;
    }
  }
  .ar-img {
    width: 100%;
    position: relative;
    img {
      width: 50%;
      display: block;
      margin: auto;
      margin-left: 20%;

      &:last-of-type {
        width: 25%;
        position: absolute;
        right: 15%;
        top: 38%;
      }
    }
  }
}

.page4 {
  padding: 7% 0;
  h1 {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 3%;

    .img-box {
      width: 100px;
      height: 100px;
      display: flex;
      justify-content: center;
      align-items: center;

      &:first-of-type {
        align-items: flex-end;
        img {
          width: 95px;
          height: 99px;
        }
      }

      &:last-of-type {
        img {
          width: 88px;
          height: 62px;
        }
      }
    }
    span {
      font-family: "ONE-Mobile-POP";
      font-size: 43px;
    }
  }
  .narr-img {
    width: 100%;
    img {
      display: block;
      width: 50%;
      margin: auto;
    }
  }
}
</style>
