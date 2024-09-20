package ua.edu.chnu.comments;

import jakarta.annotation.PostConstruct;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.springframework.http.HttpStatus;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.services.CommentService;

import java.util.ArrayList;

import static io.restassured.RestAssured.*;
import static org.hamcrest.Matchers.*;
import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.when;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CommentsApplicationTests {
    private static final CommentDTO TEST_COMMENT = new CommentDTO("test", "test"),
            UPDATED_COMMENT = new CommentDTO("updated", "updated");

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
        var comments = new ArrayList<CommentDTO>();
        comments.add(TEST_COMMENT);

        when(service.getAll()).thenReturn(comments);

        get(url)
                .then()
                .body(notNullValue())
                .body("size()", is(comments.size()))
                .body("[0].author", equalTo(TEST_COMMENT.getAuthor()))
                .body("[0].content", equalTo(TEST_COMMENT.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testGet() {
        when(service.get(1)).thenReturn(TEST_COMMENT);

        get(url + "/1")
                .then()
                .body(notNullValue())
                .body("author", equalTo(TEST_COMMENT.getAuthor()))
                .body("content", equalTo(TEST_COMMENT.getContent()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testCreate() {
        when(service.create(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        given()
                .contentType("application/json")
                .body(TEST_COMMENT)
                .post(url)
                .then()
                .body(notNullValue())
                .statusCode(HttpStatus.CREATED.value())
                .log()
                .all();
    }

    @Test
    void testUpdate() {
        doNothing().when(service).update(1, UPDATED_COMMENT);

        given()
                .contentType("application/json")
                .body(UPDATED_COMMENT)
                .put(url + "/1")
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value())
                .log()
                .all();
    }

    @Test
    void testDelete() {
        doNothing().when(service).delete(1);

        delete(url + "/1")
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value())
                .log()
                .all();
    }
}
