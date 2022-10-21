package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.Auth;
import com.ssafy.b305.domain.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface AuthRepository extends JpaRepository<Auth, Integer> {
    Optional<Auth> findByuser_email(String email);

    int deleteByuser_email(String email);
}
