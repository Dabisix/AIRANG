package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {

    Optional<User> findByUserId(String userId);
    Optional<User> findByUserEmail(String userEmail);
    void deleteByUserId(String userId);

    Optional<User> findByUserNum(int userNum);
}
