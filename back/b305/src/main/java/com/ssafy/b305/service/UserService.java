package com.ssafy.b305.service;

import com.ssafy.b305.domain.entity.Mail;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.repository.UserRepository;
import org.mindrot.jbcrypt.BCrypt;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.SQLException;
import java.util.Optional;

@Service
public class UserService {

    @Autowired
    UserRepository userRepository;

    @Autowired
    MailService mailService;

    public User findUserById(String id) {
        Optional<User> user = userRepository.findByUserId(id);
        if(user.isPresent())
            return user.get();
        return null;
    }

    public String makeTmpPw(String userId) throws SQLException {
        char[] charSet = new char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        String pwd = "";


        /* 10 자리의 랜덤 임시 비밀번호 생성 */
        int idx = 0;
        for(int i = 0; i < 10; i++){
            idx = (int) (charSet.length * Math.random());
            pwd += charSet[idx];
        }

        User user = findUserById(userId);

        String hashPw = BCrypt.hashpw(pwd, BCrypt.gensalt());

        user.setPw(hashPw);
//        updateUser(user);

        //메일 생성
        Mail mail = mailService.createMail(pwd, user.getEmail());

        //메일 보내기
        mailService.sendMail(mail);

        return pwd;
    }
}
