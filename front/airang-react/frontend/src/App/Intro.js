import React, { Component } from "react";
// import { history } from "react-router-dom";
import { getName, saveName } from "./api/localstorage";

class Intro extends Component {
  constructor(props) {
    super(props);

    this.state = {
      name: undefined,
    };
  }

  componentDidMount() {
    const name = getName();
    if (name) {
      this.setState({ name });
    }
  }

  render() {
    return (
      <div className="page-intro">
        <div id="PageIntro">
          <h2 style={{ color: "#FD9E66" }}>아이랑(AiRang) Recording Studio</h2>
          <h1>Help us build the voice(s) of Mycroft!</h1>
          <p>
            Mycroft의 오픈 소스 Mimic 기술은 Text-to-Speech 엔진이며, 서면
            텍스트를 음성 오디오로 변환합니다. 이 기술의 최신 세대는 기계 학습을
            사용합니다. 특정 언어를 말할 수 있는 모델을 만드는 기술, 훈련된
            목소리처럼 들립니다.
          </p>
          <div className="instructions">
            <i className="fas fa-book-open" />
            <h2>guide</h2>

            <p>또한 음성 녹음에 대한 다음 조언을 따르십시오.</p>
            <ul className="persona-desc">
              <li>
                좋은 마이크와 조용한 녹음실 설정을 사용하십시오(컴퓨터 팬,
                에어컨 등 없음).
              </li>
              <li>
                깨끗한 숫자/약어와 좋은 음소 적용 범위를 가진 텍스트 코퍼스를
                사용하십시오.
              </li>
              <li>
                중립적이지만 자연스러운 말투로 읽고 글자를 삼키지 마십시오.
              </li>
              <li>
                구두점으로 톤과 피치를 조정합니다. 일정한 기록 속도를
                사용하십시오.
              </li>
              <li>
                배경 소음이 있는지 정기적으로 높은 볼륨으로 녹음을 확인하십시오.
              </li>
              <li>
                정기적으로 휴식을 취하고 하루에 4시간 이상 녹음하지 마십시오
              </li>
              <li>오류없이 기록하십시오.</li>
            </ul>
            <span className="li-title">행복한 녹음 :-)</span>
          </div>
          {getName() ? this.renderWelcomeBackMsg() : this.renderInput()}
          <div className="btn_PageIntro">
            <button
              id="btn_PageIntro"
              className="btn"
              onClick={this.handleTrainMimicBtn}
            >
              Record
            </button>
          </div>
        </div>
      </div>
    );
  }

  renderInput = () => {
    return (
      <div>
        <p>시작하려면 이름을 입력하고 녹음 버튼을 누르십시오.</p>
        <input
          type="text"
          id="yourname"
          placeholder="Your Name"
          onChange={this.handleInput}
        />
      </div>
    );
  };

  renderWelcomeBackMsg = () => {
    return (
      <div>
        <p>Welcome back {this.state.name}!</p>
        <p>Hit Train Mimic to continue recording</p>
      </div>
    );
  };

  handleInput = (e) => {
    this.setState({ name: e.target.value });
  };

  handleTrainMimicBtn = () => {
    if (this.state.name === undefined) {
      alert("Please input a name before proceeding!");
    } else {
      saveName(this.state.name);
      this.props.history.push("/record");
    }
  };
}

export default Intro;
