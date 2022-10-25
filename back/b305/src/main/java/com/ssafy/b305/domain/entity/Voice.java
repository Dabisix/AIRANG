package com.ssafy.b305.domain.entity;

import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
//부모 목소리를 저장하고 있다.
public class Voice {

    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Id
    private Long v_id;

    @JsonIgnore
    @OneToOne
    @JoinColumn(name = "b_id")
    private Book book;

    @JsonIgnore
    @OneToOne
    @JoinColumn(name = "u_id")
    private User user;

    @Column
    private String voiceURL;

    public void voiceUpdate(String voiceURL) {
        this.voiceURL = voiceURL;
    }
}
