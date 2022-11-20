package com.ssafy.b305.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class BookRequestDto {
    private int aFlag;
    private int sort;
    private String keyword;
    private int pageNo;
    private List<String> paths;
}
