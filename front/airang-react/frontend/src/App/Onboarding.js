import React, { Component } from "react";
import Top from "./assets/top.png";
import Logo from "./assets/logo.png";
import MainImg from "./assets/main-img.png";
import Download from "./assets/download.png";
import Arang from "./assets/arang.png";
import Number1 from "./assets/number1.png";
import Number2 from "./assets/number2.png";
import Number3 from "./assets/number3.png";
import Number4 from "./assets/number4.png";
import Ar from "./assets/ar.gif";
import Page3Child from "./assets/page3-child.png";
import Page2Bg from "./assets/page2-bg.png";
import Narr from "./assets/narr.png";
import Hancut from "./assets/hancut.png";
import AOS from "aos";
import "aos/dist/aos.css";

class Onboarding extends Component {
  render() {
    AOS.init();

    const top = () => {
      window.scrollTo({ top: 0, behavior: "smooth" });
    };

    window.onbeforeunload = () => {
      window.scrollTo(0, 0);
    };

    return (
      <div>
        <div id="contents">
          <a class="top" onClick={top}>
            <img src={Top} alt="top" />
          </a>
          <div class="page1">
            <div class="left-box">
              <div class="logo">
                <img src={Logo} alt="logo" />
              </div>
              <div class="page1-img">
                <img src={MainImg} alt="main-img" />
              </div>
            </div>
            <div class="right-box">
              <a
                href="https://k7b305.p.ssafy.io/apk/airang.apk"
                download="airang.apk"
                class="download"
              >
                <img src={Download} alt="download" />
              </a>
              <div class="text">
                아이랑과 함께
                <br />
                <span>생생한 동화</span>를 만나보세요!
              </div>
            </div>
          </div>
          <div class="page2">
            <h1
              data-aos="fade-up"
              data-aos-delay="100"
              data-aos-duration="1200"
            >
              <div class="img-box">
                <img src={Arang} alt="arang" />
              </div>
              <div class="img-box">
                <img src={Number1} alt="number1" />
              </div>
              <span>200여 종류</span>의 다양한 <span>&nbsp;동화</span>들!
            </h1>
            <div
              class="page2-bg"
              data-aos="fade-up"
              data-aos-duration="2000"
              data-aos-delay="120"
            >
              <img src={Page2Bg} alt="page2 img" />
            </div>
          </div>
          <div class="page3">
            <h1
              data-aos="fade-up"
              data-aos-delay="140"
              data-aos-duration="1000"
            >
              <div class="img-box">
                <img src={Arang} alt="arang" />
              </div>
              <div class="img-box">
                <img src={Number2} alt="number1" />
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
              <img src={Ar} alt="ar" />
              <img src={Page3Child} alt="child" />
            </div>
          </div>
          <div class="page4">
            <h1
              data-aos="fade-up"
              data-aos-delay="180"
              data-aos-duration="1000"
            >
              <div class="img-box">
                <img src={Arang} alt="arang" />
              </div>
              <div class="img-box">
                <img src={Number3} alt="number1" />
              </div>
              <span>다양한 목소리</span>로 동화 몰입도 향상!
            </h1>
            <div
              class="narr-img"
              data-aos="fade-up"
              data-aos-delay="200"
              data-aos-duration="1000"
            >
              <img src={Narr} alt="narr" />
            </div>
          </div>
          <div class="page5">
            <h1
              data-aos="fade-up"
              data-aos-delay="220"
              data-aos-duration="1000"
            >
              <div class="img-box">
                <img src={Arang} alt="arang" />
              </div>
              <div class="img-box">
                <img src={Number4} alt="number1" />
              </div>
              <span>아이랑 한컷</span>으로 추억 남기기!
            </h1>
            <div
              class="one-cut-img"
              data-aos="fade-up"
              data-aos-delay="240"
              data-aos-duration="1000"
            >
              <img src={Hancut} alt="hancut" />
              <p>원하는 캐릭터를 선택하고 프레임도 골라보세요!</p>
            </div>
          </div>
        </div>
        <div class="footer">
          <img src={Arang} alt="arang" />
          <b>아이랑 (AiRang)</b> <br />
          Frontend | 김수빈 우상욱 이화연 정수빈 <br />
          Backend | 박서은 육다빈 <br />
          Copyright 2022 AiRang. All rights reserved.
        </div>
      </div>
    );
  }
}

export default Onboarding;
