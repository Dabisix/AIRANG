package com.ssafy.b305.domain.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
@Builder
public class Mail {
    private String toAddress; //수신인
    private String title; //제목
    private String message; //내용
    private String fromAddress; //송신인
}
