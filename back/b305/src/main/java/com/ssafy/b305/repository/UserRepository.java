package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {

//    Optional<User> findByU_id(String userId);
    Optional<User> findByEmail(String userEmail);
    void deleteByEmail(String userEmail);

//    Optional<User> findByUserNum(int userNum);
}
