package ua.edu.chnu.comments;

import io.restassured.RestAssured;
import jakarta.annotation.PostConstruct;
import org.hamcrest.Matchers;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.springframework.http.HttpStatus;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.services.CommentService;

import java.util.ArrayList;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CommentsApplicationTests {
    @LocalServerPort
    private int port;

    @MockBean
    private CommentService service;

    public String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/api/comments";
    }

    @Test
    void testGetAll() {
        var comments = new ArrayList<Comment>();

        Comment comment = createComment("test", "test");
        comments.add(comment);

        Mockito.when(service.getAll()).thenReturn(comments);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(comments.size()))
                .body("[0].author", Matchers.equalTo(comment.getAuthor()))
                .body("[0].content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testGet() {
        Comment comment = createComment("test", "test");

        Mockito.when(service.get(1)).thenReturn(comment);

        RestAssured.get(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("author", Matchers.equalTo(comment.getAuthor()))
                .body("content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testCreate() {
        Comment comment = createComment("test", "test");

        Mockito.when(service.create(comment)).thenReturn(comment);

        RestAssured.given()
                .contentType("application/json")
                .body(comment)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("author", Matchers.equalTo(comment.getAuthor()))
                .body("content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.CREATED.value())
                .log()
                .all();
    }

    @Test
    void testUpdate() {
        Comment comment = createComment("updated", "updated");

        Mockito.when(service.update(1, comment)).thenReturn(comment);

        RestAssured.given()
                .contentType("application/json")
                .body(comment)
                .put(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("author", Matchers.equalTo(comment.getAuthor()))
                .body("content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testDelete() {
        Comment comment = createComment("test", "test");

        Mockito.when(service.delete(1)).thenReturn(comment);

        RestAssured.delete(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("author", Matchers.equalTo(comment.getAuthor()))
                .body("content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    private static Comment createComment(String author, String content) {
        Comment comment = new Comment();
        comment.setAuthor(author);
        comment.setContent(content);

        return comment;
    }
}
