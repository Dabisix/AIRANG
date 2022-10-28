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
public class Auth {

    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Id
    private int a_id;
    private String refreshToken;

    @JsonIgnore
    @OneToOne
    @JoinColumn(name = "u_id")
    private User user;

    public void refreshUpdate(String refreshToken) {
        this.refreshToken = refreshToken;
    }
}
