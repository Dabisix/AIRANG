package com.ssafy.b305.service;

import com.ssafy.b305.domain.entity.Auth;
import com.ssafy.b305.repository.AuthRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class AuthService {

    @Autowired
    AuthRepository authRepository;

    public Optional<Auth> findUser(String email) {
        System.out.println(email);
        return authRepository.findByuser_email(email);
    }

    public void delete(Auth auth) {
        authRepository.delete(auth);
    }
    public void save(Auth auth) {
        authRepository.save(auth);
    }
}
