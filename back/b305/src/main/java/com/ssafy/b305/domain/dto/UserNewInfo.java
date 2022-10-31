package com.ssafy.b305.domain.dto;

import lombok.*;

@Getter
@ToString
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class UserNewInfo {
    String name;
    String pw;
}
