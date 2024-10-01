package ua.edu.chnu.comments_api_java;

import io.restassured.RestAssured;
import jakarta.annotation.PostConstruct;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.hamcrest.Matchers;
import org.springframework.http.HttpStatus;
import ua.edu.chnu.comments_api_java.comments.Comment;
import ua.edu.chnu.comments_api_java.comments.CommentNotFoundException;
import ua.edu.chnu.comments_api_java.comments.CommentService;

import java.util.List;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CommentControllerIntegrationTest {
    private static final int TEST_ID = 1;

    private static final Comment TEST_COMMENT = new Comment(TEST_ID, "test", "test"),
            UPDATED_COMMENT = new Comment(TEST_ID, "updated", "updated");

    private static final CommentNotFoundException TEST_EXCEPTION = new CommentNotFoundException(TEST_ID);

    @LocalServerPort
    private int port;

    @MockBean
    private CommentService service;

    public String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/java/api/comments";
    }

    @Test
    void testReadAll() {
        var comments = List.of(TEST_COMMENT);
        Mockito.when(service.readAll()).thenReturn(comments);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(comments.size()))
                .body("[0].id", Matchers.is(TEST_ID))
                .body("[0].author", Matchers.equalTo(TEST_COMMENT.getAuthor()))
                .body("[0].content", Matchers.equalTo(TEST_COMMENT.getContent()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testRead() {
        Mockito.when(service.read(TEST_ID)).thenReturn(TEST_COMMENT);

        RestAssured.get(url + "/" + TEST_ID)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.is(TEST_ID))
                .body("author", Matchers.equalTo(TEST_COMMENT.getAuthor()))
                .body("content", Matchers.equalTo(TEST_COMMENT.getContent()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testReadNotFound() {
        Mockito.when(service.read(TEST_ID)).thenThrow(TEST_EXCEPTION);

        RestAssured.get(url + "/" + TEST_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo(TEST_EXCEPTION.getMessage()))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testCreate() {
        Mockito.when(service.create(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        RestAssured.given()
                .contentType("application/json")
                .body(TEST_COMMENT)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.is(TEST_ID))
                .body("author", Matchers.equalTo(TEST_COMMENT.getAuthor()))
                .body("content", Matchers.equalTo(TEST_COMMENT.getContent()))
                .statusCode(HttpStatus.CREATED.value());
    }

    @Test
    void testUpdate() {
        Mockito.doNothing().when(service).update(TEST_ID, UPDATED_COMMENT);

        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COMMENT)
                .put(url + "/" + TEST_ID)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testUpdateNotFound() {
        Mockito.doThrow(TEST_EXCEPTION).when(service).update(TEST_ID, UPDATED_COMMENT);

        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COMMENT)
                .put(url + "/" + TEST_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo(TEST_EXCEPTION.getMessage()))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDelete() {
        Mockito.doNothing().when(service).delete(TEST_ID);

        RestAssured.delete(url + "/" + TEST_ID)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testDeleteNotFound() {
        Mockito.doThrow(TEST_EXCEPTION).when(service).delete(TEST_ID);

        RestAssured.delete(url + "/" + TEST_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo(TEST_EXCEPTION.getMessage()))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }
}