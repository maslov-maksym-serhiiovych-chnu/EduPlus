package ua.edu.chnu.courses_api;

import io.restassured.RestAssured;
import jakarta.annotation.PostConstruct;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.context.annotation.Import;
import org.springframework.http.HttpStatus;
import org.testcontainers.containers.PostgreSQLContainer;
import org.hamcrest.Matchers;
import ua.edu.chnu.courses_api.courses.Course;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CourseControllerIntegrationTest {
    private static final int READ_ID = 1, UPDATED_ID = 2, DELETED_ID = 3, NOT_FOUND_ID = Integer.MAX_VALUE;
    private static final String NOT_FOUND_MESSAGE = "course not found by id: " + NOT_FOUND_ID,
            DELETED_NOT_FOUND_MESSAGE = "course not found by id: " + DELETED_ID;

    private static final Course READ = new Course(READ_ID, "read", "read"),
            CREATED = new Course(0, "created", "created"),
            UPDATED = new Course(UPDATED_ID, "updated", "updated");

    private static final String TEST_DATA = """
            INSERT INTO courses (name, description) VALUES ('read', 'read'),
                           ('should-updated', 'should-updated'),
                           ('deleted', 'deleted')
            """;

    private static boolean testDataInserted;

    @LocalServerPort
    private int port;

    @Autowired
    private PostgreSQLContainer<?> container;

    private String url;

    @PostConstruct
    public void init() throws SQLException {
        url = "http://localhost:" + port + "/api/courses";

        if (testDataInserted) {
            return;
        }

        try (Connection connection = DriverManager.getConnection(
                container.getJdbcUrl(),
                container.getUsername(),
                container.getPassword()
        ); Statement statement = connection.createStatement()) {
            statement.execute(TEST_DATA);
        }

        testDataInserted = true;
    }

    @Test
    void testReadAll() {
        RestAssured.get(url)
                .then()
                .body("[0]", Matchers.notNullValue())
                .body("[0].id", Matchers.equalTo(READ_ID))
                .body("[0].name", Matchers.equalTo(READ.getName()))
                .body("[0].description", Matchers.equalTo(READ.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testRead() {
        RestAssured.get(url + "/" + READ_ID)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(READ.getId()))
                .body("name", Matchers.equalTo(READ.getName()))
                .body("description", Matchers.equalTo(READ.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testReadNotFound() {
        RestAssured.get(url + "/" + NOT_FOUND_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testCreate() {
        RestAssured.given()
                .contentType("application/json")
                .body(CREATED)
                .post(url)
                .then()
                .body("name", Matchers.equalTo(CREATED.getName()))
                .body("description", Matchers.equalTo(CREATED.getDescription()))
                .statusCode(HttpStatus.CREATED.value());
    }

    @Test
    void testUpdate() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED)
                .put(url + "/" + UPDATED_ID)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + UPDATED_ID)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.equalTo(UPDATED_ID))
                .body("name", Matchers.equalTo(UPDATED.getName()))
                .body("description", Matchers.equalTo(UPDATED.getDescription()));
    }

    @Test
    void testUpdateNotFound() {
        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED)
                .put(url + "/" + NOT_FOUND_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDelete() {
        RestAssured.delete(url + "/" + DELETED_ID)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());

        RestAssured.get(url + "/" + DELETED_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(DELETED_NOT_FOUND_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testDeleteNotFound() {
        RestAssured.delete(url + "/" + NOT_FOUND_ID)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.is(NOT_FOUND_MESSAGE))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }
}