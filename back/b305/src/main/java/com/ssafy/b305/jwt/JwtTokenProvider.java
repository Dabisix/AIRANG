package com.ssafy.b305.jwt;

import com.ssafy.b305.domain.dto.TokenResponse;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.repository.AuthRepository;
import com.ssafy.b305.repository.UserRepository;
import io.jsonwebtoken.*;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;
import javax.servlet.http.HttpServletRequest;
import javax.xml.bind.DatatypeConverter;
import java.io.UnsupportedEncodingException;
import java.util.Base64;
import java.util.Date;
import java.util.Optional;

@RequiredArgsConstructor
@Component
public class JwtTokenProvider {

    @Autowired
    AuthRepository authRepository;

    @Autowired
    UserRepository userRepository;


    public static final Logger logger = LoggerFactory.getLogger(JwtTokenProvider.class);

    @Value("${secret.access}")
    private String SECRET_KEY;
    @Value("${secret.refresh}")
    private String REFRESH_KEY;

    private final long ACCESS_TOKEN_VALID_TIME = 60 * 60 * 1000L;   // 60분
    private final long REFRESH_TOKEN_VALID_TIME = 60 * 60 * 24 * 7 * 1000L;   // 1주

    // 객체 초기화, secretKey를 Base64로 인코딩한다.
    @PostConstruct
    protected void init() {
        SECRET_KEY = Base64.getEncoder().encodeToString(SECRET_KEY.getBytes());
        REFRESH_KEY = Base64.getEncoder().encodeToString(REFRESH_KEY.getBytes());
    }

    // JWT 토큰 생성
    public String createAccessToken(String userId) {
        Claims claims = Jwts.claims();//.setSubject(userPk); // JWT payload 에 저장되는 정보단위
        claims.put("userId", userId);
        Date now = new Date();
        return Jwts.builder()
                .setClaims(claims) // 정보 저장
                .setIssuedAt(now) // 토큰 발행 시간 정보
                .setExpiration(new Date(now.getTime() + ACCESS_TOKEN_VALID_TIME)) // set Expire Time
                .signWith(SignatureAlgorithm.HS256, SECRET_KEY)  // 사용할 암호화 알고리즘과
                .compact();
    }

    public String createRefreshToken(String userId) {
        Claims claims = Jwts.claims();
        claims.put("userId", userId); //
        Date now = new Date();
        Date expiration = new Date(now.getTime() + REFRESH_TOKEN_VALID_TIME);

        return Jwts.builder()
                .setClaims(claims)
                .setIssuedAt(now)
                .setExpiration(expiration)
                .signWith(SignatureAlgorithm.HS256, REFRESH_KEY)
                .compact();
    }

    // Request의 Header에서 token 값을 가져옵니다. "X-AUTH-TOKEN" : "TOKEN값'
    public String resolveAccessToken(HttpServletRequest request) {
        return request.getHeader("access-token");
    }

    public String resolveRefreshToken(HttpServletRequest request) {
        return request.getHeader("refresh-token");
    }


    public Claims getClaimsFormToken(String token) {
        return Jwts.parser()
                .setSigningKey(DatatypeConverter.parseBase64Binary(SECRET_KEY))
                .parseClaimsJws(token)
                .getBody();
    }

    public Claims getClaimsToken(String token) {
        return Jwts.parser()
                .setSigningKey(DatatypeConverter.parseBase64Binary(REFRESH_KEY))
                .parseClaimsJws(token)
                .getBody();
    }

    public boolean isValidAccessToken(String token) {
        try {
            Claims accessClaims = getClaimsFormToken(token);
            return true;
        } catch (ExpiredJwtException exception) {
            return false;
        } catch (JwtException exception) {
            return false;
        } catch (NullPointerException exception) {
            return false;
        }
    }
    public boolean isValidRefreshToken(String token) {
        try {
            Claims accessClaims = getClaimsToken(token);
            return true;
        } catch (ExpiredJwtException exception) {
            return false;
        } catch (JwtException exception) {
            return false;
        } catch (NullPointerException exception) {
            return false;
        }
    }
    public boolean isOnlyExpiredToken(String token) {
        try {
            Claims accessClaims = getClaimsFormToken(token);
            return false;
        } catch (ExpiredJwtException exception) {
            return true;
        } catch (JwtException exception) {
            return false;
        } catch (NullPointerException exception) {
            return false;
        }
    }

    public String getUserID(String jwt) {
        Jws<Claims> claims = Jwts.parser().setSigningKey(SECRET_KEY).parseClaimsJws(jwt);
        String userId = (String) claims.getBody().get("userId");
        return userId;
    }

//    public int getUserNum(String jwt) {
//        String userId = getUserID(jwt);
//
//        Optional<User> oUser = userRepository.findByUserId(userId);
//        if (!oUser.isPresent()) {
//            return -1;
//        }
//
//        int userNum = oUser.get().getUserNum();
//        return userNum;
//    }


    private byte[] generateKey() {
        byte[] key = null;
        try {
            key = SECRET_KEY.getBytes("UTF-8");
        } catch (UnsupportedEncodingException e) {
            if (logger.isInfoEnabled()) {
                e.printStackTrace();
            } else {
                logger.error("Making JWT Key Error ::: {}", e.getMessage());
            }
        }
        return key;
    }

    public TokenResponse issueAccessToken(HttpServletRequest request){
        String accessToken = resolveAccessToken(request);
        String refreshToken = resolveRefreshToken(request);

        //accessToken이 만료됐고 refreshToken이 맞으면 accessToken을 새로 발급(refreshToken의 내용을 통해서)
        if(isValidRefreshToken(refreshToken)){     //들어온 Refresh 토큰이 유효한지

            Claims claimsToken = getClaimsToken(refreshToken);
            String userId = (String)claimsToken.get("userId");

            Optional<User> user = userRepository.findByUserId(userId);
            String tokenFromDB = authRepository.findByuser_userId(user.get().getU_id()).get().getRefreshToken();

            if(refreshToken.equals(tokenFromDB)) {   //DB의 refresh토큰과 지금들어온 토큰이 같은지 확인
                accessToken = createAccessToken(userId);
            }
            else{
                //DB의 Refresh토큰과 들어온 Refresh토큰이 다르면 중간에 변조된 것임
                //예외발생
                return null;
            }
        }
        else{
            //입력으로 들어온 Refresh 토큰이 유효하지 않음
            return null;
        }

        return TokenResponse.builder()
//                .ACCESS_TOKEN(accessToken)
                .REFRESH_TOKEN(refreshToken)
                .build();
    }
}
