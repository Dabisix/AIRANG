package com.ssafy.b305.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class BookRequestDto {
    private int aFlag;
    private boolean cntSort;
    private boolean titleSort;
    private String keyword;
}
