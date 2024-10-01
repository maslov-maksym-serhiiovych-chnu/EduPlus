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
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
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
    void testReadAll() {
        var comments = new ArrayList<Comment>();

        Comment comment = createComment("test", "test");
        comments.add(comment);

        Mockito.when(service.readAll()).thenReturn(comments);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(comments.size()))
                .body("[0].author", Matchers.equalTo(comment.getAuthor()))
                .body("[0].content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testRead() {
        Comment comment = createComment("test", "test");
        Mockito.when(service.read(1)).thenReturn(comment);

        RestAssured.get(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("author", Matchers.equalTo(comment.getAuthor()))
                .body("content", Matchers.equalTo(comment.getContent()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testReadNotFound() {
        int id = Integer.MAX_VALUE;
        Mockito.when(service.read(id)).thenThrow(new CommentNotFoundByIdException(id));

        RestAssured.get(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("comment not found by id: " + id))
                .statusCode(HttpStatus.NOT_FOUND.value());
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
                .statusCode(HttpStatus.CREATED.value());
    }

    @Test
    void testUpdate() {
        Comment comment = createComment("updated", "updated");

        Mockito.doNothing().when(service).update(1, comment);

        RestAssured.given()
                .contentType("application/json")
                .body(comment)
                .put(url + "/1")
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testUpdateNotFound() {
        int id = Integer.MAX_VALUE;
        Comment comment = createComment("updated", "updated");
        Mockito.doThrow(new CommentNotFoundByIdException(id)).when(service).update(id, comment);

        RestAssured.given()
                .contentType("application/json")
                .body(comment)
                .put(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("comment not found by id: " + id))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDelete() {
        Mockito.doNothing().when(service).delete(1);

        RestAssured.delete(url + "/1")
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testDeleteNotFound() {
        int id = Integer.MAX_VALUE;
        Mockito.doThrow(new CommentNotFoundByIdException(id)).when(service).delete(id);

        RestAssured.delete(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("comment not found by id: " + id))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    private static Comment createComment(String author, String content) {
        Comment comment = new Comment();
        comment.setAuthor(author);
        comment.setContent(content);

        return comment;
    }
}
