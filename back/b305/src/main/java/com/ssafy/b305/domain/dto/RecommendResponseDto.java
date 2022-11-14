package com.ssafy.b305.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@AllArgsConstructor
public class RecommendResponseDto {
    private BookInfoResponseDto targetBook;
    private List<BookInfoResponseDto> recList = new ArrayList<BookInfoResponseDto>();
}
