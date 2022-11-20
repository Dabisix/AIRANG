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
        <a
          href="https://k7b305.p.ssafy.io/apk/airang.apk"
          download="airang.apk"
          class="download"
        >
          <img src="./assets/download.png" alt="download" />
        </a>
        <div class="text">
          아이랑과 함께<br />
          <span>생생한 동화</span>를 만나보세요!
        </div>
      </div>
    </div>
    <div class="page2">
      <h1 data-aos="fade-up" data-aos-delay="100" data-aos-duration="1200">
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
        data-aos-delay="120"
      >
        <img src="./assets/page2-bg.png" alt="page2 img" />
      </div>
    </div>
    <div class="page3">
      <h1 data-aos="fade-up" data-aos-delay="140" data-aos-duration="1000">
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
        data-aos-delay="160"
        data-aos-duration="1000"
      >
        <img src="./assets/ar.gif" alt="ar" />
        <img src="./assets/page3-child.png" alt="child" />
      </div>
    </div>
    <div class="page4">
      <h1 data-aos="fade-up" data-aos-delay="180" data-aos-duration="1000">
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
        data-aos-delay="200"
        data-aos-duration="1000"
      >
        <img src="./assets/narr.png" alt="narr" />
      </div>
    </div>
    <div class="page5">
      <h1 data-aos="fade-up" data-aos-delay="220" data-aos-duration="1000">
        <div class="img-box">
          <img src="./assets/arang.png" alt="arang" />
        </div>
        <div class="img-box">
          <img src="./assets/number4.png" alt="number1" />
        </div>
        <span>아이랑 한컷</span>으로 추억 남기기!
      </h1>
      <div
        class="one-cut-img"
        data-aos="fade-up"
        data-aos-delay="240"
        data-aos-duration="1000"
      >
        <img src="./assets/hancut.png" alt="hancut" />
        <p>원하는 캐릭터를 선택하고 프레임도 골라보세요!</p>
      </div>
    </div>
  </div>
  <div class="footer">
    <img src="./assets/arang.png" alt="arang" />

    <b>아이랑 (AiRang)</b> <br />
    Frontend | 김수빈 우상욱 이화연 정수빈 <br />
    Backend | 박서은 육다빈 <br />
    Copyright 2022 AiRang. All rights reserved.
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

  @media (max-width: 480px) {
    width: 80px;
    height: 110px;
  }

  @media (max-width: 768px) {
    width: 80px;
    height: 110px;
  }

  @media all and (min-width: 768px) and (max-width: 1024px) {
    width: 100px;
    height: 130px;
  }

  img {
    width: 100%;
    height: 100%;
  }
}

#contents > div > h1 {
  display: flex;
  justify-content: center;
  align-items: center;
  align-content: center;
  margin: auto;
  margin-bottom: 3%;

  @media all and (min-width: 768px) and (max-width: 1024px) {
    font-size: 30px;
  }

  @media (max-width: 768px) {
    font-size: 25px;
  }
  @media (max-width: 480px) {
    font-size: 18px;
  }

  .img-box {
    width: 95px;
    height: 99px;
    display: flex;
    justify-content: center;
    align-items: center;

    @media all and (min-width: 768px) and (max-width: 1024px) {
      width: 80px;
      height: 85px;
    }

    @media (max-width: 768px) {
      width: 65px;
      height: 70px;
    }

    @media (max-width: 480px) {
      width: 35px;
      height: 40px;
    }

    &:first-of-type {
      img {
        width: 100%;
        height: 100%;
      }
    }

    &:last-of-type {
      width: 88px;
      height: 62px;

      @media all and (min-width: 768px) and (max-width: 1024px) {
        width: 78px;
        height: 57px;
      }

      @media (max-width: 768px) {
        width: 68px;
        height: 47px;
      }

      @media (max-width: 480px) {
        width: 50px;
        height: 35px;
      }
      img {
        width: 100%;
        height: 100%;
      }
    }
  }
  span {
    font-family: "ONE-Mobile-POP";
    font-size: 43px;
    @media all and (min-width: 768px) and (max-width: 1024px) {
      font-size: 30px;
    }

    @media (max-width: 768px) {
      font-size: 25px;
    }

    @media (max-width: 480px) {
      font-size: 18px;
    }

    &:last-of-type {
      color: #63432c;
    }
  }
}

.page1 {
  display: flex;
  background: url("./assets/main-bg.png");
  background-size: contain;
  background-repeat: round;
  height: 100vh;

  @media (max-width: 768px) {
    background-size: cover;
  }

  @media all and (min-width: 768px) and (max-width: 1024px) {
    height: auto;
    padding: 10% 0;
  }

  .left-box {
    width: 70%;
    display: flex;
    flex-direction: column;
    padding-left: 3%;

    @media all and (min-width: 768px) and (max-width: 1024px) {
      width: 65%;
    }

    @media (max-width: 768px) {
      width: 60%;
    }

    @media (max-width: 480px) {
      width: 50%;
    }

    .logo {
      height: 25%;
      width: 100%;
      display: flex;
      align-items: center;

      img {
        margin: 5% 0 0 5%;
        display: block;
        width: 150px;

        @media (max-width: 768px) {
          width: 120px;
        }
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

    @media all and (min-width: 768px) and (max-width: 1024px) {
      width: 35%;
    }

    @media (max-width: 768px) {
      width: 40%;
    }

    @media (max-width: 480px) {
      width: 50%;
    }

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
      @media all and (min-width: 768px) and (max-width: 1024px) {
        font-size: 25px;
      }
      @media (max-width: 768px) {
        font-size: 22px;
      }
      span {
        font-family: "ONE-Mobile-POP";
        color: #63432c;
      }
    }
  }
}

.page2 {
  padding: 7% 0;
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
  .narr-img {
    width: 100%;
    img {
      display: block;
      width: 50%;
      margin: auto;
    }
  }
}

.page5 {
  padding: 7% 0 8%;
  height: auto;
  background-color: #ffe9d8;

  .one-cut-img {
    width: 50%;
    margin: auto;

    img {
      width: 100%;
    }
    p {
      margin-top: 6%;
      font-size: 23px;
      text-align: center;
      font-weight: bold;

      @media all and (min-width: 768px) and (max-width: 1024px) {
        font-size: 20px;
      }

      @media (max-width: 768px) {
        font-size: 16px;
      }

      @media (max-width: 480px) {
        font-size: 14px;
      }
    }
  }
}

.footer {
  padding: 2% 0;
  font-size: 15px;
  img {
    display: block;
    margin: auto;
    width: 5%;
    height: 5%;
  }
  text-align: center;
  line-height: 1.5;

  @media (max-width: 768px) {
    font-size: 13px;
  }

  @media (max-width: 480px) {
    font-size: 11px;
  }
}
</style>
