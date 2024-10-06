package ua.edu.chnu.comments_api;

import io.restassured.RestAssured;
import jakarta.annotation.PostConstruct;
import org.hamcrest.Matchers;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.springframework.http.HttpStatus;
import ua.edu.chnu.comments_api.comments.Comment;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CommentControllerIntegrationTest {
    private static final String NOT_FOUND_COMMENT_ID = "not-found-id",
            NOT_FOUND_COMMENT_MESSAGE = "comment not found by id: " + NOT_FOUND_COMMENT_ID;

    private static final Comment READ_COMMENT = new Comment(null, "read", 1),
            SHOULD_UPDATED_COMMENT = new Comment(null, "should-updated", 2),
            DELETED_COMMENT = new Comment(null, "deleted", 3),
            CREATED_COMMENT = new Comment(null, "created", 4),
            UPDATED_COMMENT = new Comment(null, "updated", 5);

    private static boolean testDataCreated;
    private static String readCommentId, shouldUpdatedCommentId, deletedCommentId, deletedCommentNotFoundMessage;

    @LocalServerPort
    private int port;

    private String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/api/comments";

        if (testDataCreated) {
            return;
        }

        readCommentId = RestAssured.given()
                .contentType("application/json")
                .body(READ_COMMENT)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .getString("id");

        shouldUpdatedCommentId = RestAssured.given()
                .contentType("application/json")
                .body(SHOULD_UPDATED_COMMENT)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .getString("id");

        deletedCommentId = RestAssured.given()
                .contentType("application/json")
                .body(DELETED_COMMENT)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .getString("id");

        deletedCommentNotFoundMessage = "comment not found by id: " + deletedCommentId;

        testDataCreated = true;
    }

    @Test
    void testReadAll() {
        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("[0]", Matchers.notNullValue())
                .body("[0].id", Matchers.equalTo(readCommentId))
                .body("[0].content", Matchers.equalTo(READ_COMMENT.getContent()))
                .body("[0].courseId", Matchers.equalTo(READ_COMMENT.getCourseId()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testCreate() {
        String id = RestAssured.given()
                .contentType("application/json")
                .body(CREATED_COMMENT)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("content", Matchers.equalTo(CREATED_COMMENT.getContent()))
                .body("courseId", Matchers.equalTo(CREATED_COMMENT.getCourseId()))
                .statusCode(HttpStatus.CREATED.value())
                .extract()
                .jsonPath()
                .getString("id");

        RestAssured.get(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(id))
                .body("content", Matchers.equalTo(CREATED_COMMENT.getContent()))
                .body("courseId", Matchers.equalTo(CREATED_COMMENT.getCourseId()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testUpdate() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COMMENT)
                .put(url + "/" + shouldUpdatedCommentId)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + shouldUpdatedCommentId)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(shouldUpdatedCommentId))
                .body("content", Matchers.equalTo(UPDATED_COMMENT.getContent()))
                .body("courseId", Matchers.equalTo(UPDATED_COMMENT.getCourseId()));
    }

    @Test
    void testUpdateNotFound() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COMMENT)
                .put(url + "/" + NOT_FOUND_COMMENT_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_COMMENT_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDelete() {
        RestAssured.delete(url + "/" + deletedCommentId)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + deletedCommentId)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(deletedCommentNotFoundMessage))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDeleteNotFound() {
        RestAssured.delete(url + "/" + NOT_FOUND_COMMENT_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_COMMENT_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }
}