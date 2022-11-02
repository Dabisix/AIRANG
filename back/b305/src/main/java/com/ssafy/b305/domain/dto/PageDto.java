package com.ssafy.b305.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@AllArgsConstructor
public class PageDto {
    private List<BookInfoResponseDto> booklist;
    private int limit;
}
