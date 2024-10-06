package ua.edu.chnu.courses_api;

import io.restassured.RestAssured;
import jakarta.annotation.PostConstruct;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.springframework.http.HttpStatus;
import org.hamcrest.Matchers;
import ua.edu.chnu.courses_api.courses.Course;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CourseControllerIntegrationTest {
    private static final int NOT_FOUND_COURSE_ID = Integer.MAX_VALUE;
    private static final String NOT_FOUND_COURSE_MESSAGE = "course not found by id: " + NOT_FOUND_COURSE_ID;

    private static final Course READ_COURSE = new Course(0, "read", "read"),
            SHOULD_UPDATED_COURSE = new Course(0, "should-updated", "should-updated"),
            DELETED_COURSE = new Course(0, "deleted", "deleted"),
            CREATED_COURSE = new Course(0, "created", "created"),
            UPDATED_COURSE = new Course(0, "updated", "updated");

    private static boolean testDataCreated;
    private static int readCourseId, shouldUpdatedCourseId, deletedCourseId;
    private static String deletedCourseNotFoundMessage;

    @LocalServerPort
    private int port;

    private String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/api/courses";

        if (testDataCreated) {
            return;
        }

        readCourseId = RestAssured.given()
                .contentType("application/json")
                .body(READ_COURSE)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .get("id");

        shouldUpdatedCourseId = RestAssured.given()
                .contentType("application/json")
                .body(SHOULD_UPDATED_COURSE)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .get("id");

        deletedCourseId = RestAssured.given()
                .contentType("application/json")
                .body(DELETED_COURSE)
                .post(url)
                .then()
                .extract()
                .jsonPath()
                .get("id");

        deletedCourseNotFoundMessage = "course not found by id: " + deletedCourseId;

        testDataCreated = true;
    }

    @Test
    void testReadAll() {
        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("[0]", Matchers.notNullValue())
                .body("[0].id", Matchers.equalTo(readCourseId))
                .body("[0].name", Matchers.equalTo(READ_COURSE.getName()))
                .body("[0].description", Matchers.equalTo(READ_COURSE.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testCreate() {
        int id = RestAssured.given()
                .contentType("application/json")
                .body(CREATED_COURSE)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(CREATED_COURSE.getName()))
                .body("description", Matchers.equalTo(CREATED_COURSE.getDescription()))
                .statusCode(HttpStatus.CREATED.value())
                .extract()
                .jsonPath()
                .get("id");

        RestAssured.get(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(id))
                .body("name", Matchers.equalTo(CREATED_COURSE.getName()))
                .body("description", Matchers.equalTo(CREATED_COURSE.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testUpdate() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COURSE)
                .put(url + "/" + shouldUpdatedCourseId)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + shouldUpdatedCourseId)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(shouldUpdatedCourseId))
                .body("name", Matchers.equalTo(UPDATED_COURSE.getName()))
                .body("description", Matchers.equalTo(UPDATED_COURSE.getDescription()));
    }

    @Test
    void testUpdateNotFound() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COURSE)
                .put(url + "/" + NOT_FOUND_COURSE_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_COURSE_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDelete() {
        RestAssured.delete(url + "/" + deletedCourseId)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + deletedCourseId)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(deletedCourseNotFoundMessage))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDeleteNotFound() {
        RestAssured.delete(url + "/" + NOT_FOUND_COURSE_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_COURSE_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }
}