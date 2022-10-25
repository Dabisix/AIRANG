package com.ssafy.b305.service;

import com.ssafy.b305.domain.dto.UserNewInfo;

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


    public boolean registUser(User user) {
        boolean result = false;

        // 올바른 이메일 형식이 아닐 경우
//        if(!user.getUserEmail().contains("@")) {
//            return result;
//        }

//        if( user.getUserPw().length() < 4) {
//            return result;
//        }

        //비밀번호 암호화
        try {
            String hashPw = BCrypt.hashpw(user.getPw(), BCrypt.gensalt());
            user.setPw(hashPw);
            userRepository.save(user);

            result = true;
        } catch (Exception e) {

        }


        return result;
    }

    public User findUserById(String email) {
        Optional<User> user = userRepository.findByEmail(email);
        if(user.isPresent())
            return user.get();
        return null;
    }

    public boolean deleteUser(String email) {
        boolean result = true;

        try {
            userRepository.deleteByEmail(email);
        } catch (Exception e) {
            result = false;
        }

        return result;
    }

    public String login(User user) throws SQLException {
        Optional<User> oUser = userRepository.findByEmail(user.getEmail());

        // 해당 id의 user가 있으면
        if (oUser.isPresent()) {
            User u = oUser.get();
            // 비밀번호가 틀리면
            if(!BCrypt.checkpw(user.getPw(), u.getPw())) {
                return "pwErr";
            }
            String userId = u.getEmail();
            return userId;
        }
        // 해당 id의 user가 없으면
        return "noId";
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
        userRepository.save(user);

        //메일 생성
        Mail mail = mailService.createMail(pwd, user.getEmail());

        //메일 보내기
        mailService.sendMail(mail);

        return pwd;
    }

    public int updateUser(String email, UserNewInfo newInfo) {
        Optional<User> oUser = userRepository.findByEmail(email);

        if (oUser.isPresent()) {
            User u = oUser.get();
            if(newInfo.getName() != "")
                u.setName(newInfo.getName());
            if(newInfo.getPw() != "")
                u.setPw(BCrypt.hashpw(newInfo.getPw(), BCrypt.gensalt()));

            userRepository.save(u);

            return 1;
        }

        return 0;
    }

    public User myPage(String email) {
        Optional<User> oUser = userRepository.findByEmail(email);

        if(oUser.isPresent())
            return oUser.get();
        else return null;
    }

}
