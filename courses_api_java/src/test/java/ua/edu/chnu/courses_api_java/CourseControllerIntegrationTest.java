package ua.edu.chnu.courses_api_java;

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
import ua.edu.chnu.courses_api_java.courses.Course;
import ua.edu.chnu.courses_api_java.courses.CourseNotFoundException;
import ua.edu.chnu.courses_api_java.courses.CourseService;

import java.util.List;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CourseControllerIntegrationTest {
    private static final int TEST_ID = 1;

    private static final Course TEST_COURSE = new Course(TEST_ID, "test", "test"),
            UPDATED_COURSE = new Course(TEST_ID, "updated", "updated");

    private static final CourseNotFoundException TEST_EXCEPTION = new CourseNotFoundException(TEST_ID);

    @LocalServerPort
    private int port;

    @MockBean
    private CourseService service;

    public String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/java/api/courses";
    }

    @Test
    void testReadAll() {
        var courses = List.of(TEST_COURSE);
        Mockito.when(service.readAll()).thenReturn(courses);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(courses.size()))
                .body("[0].id", Matchers.is(TEST_ID))
                .body("[0].name", Matchers.equalTo(TEST_COURSE.getName()))
                .body("[0].description", Matchers.equalTo(TEST_COURSE.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testRead() {
        Mockito.when(service.read(TEST_ID)).thenReturn(TEST_COURSE);

        RestAssured.get(url + "/" + TEST_ID)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.is(TEST_ID))
                .body("name", Matchers.equalTo(TEST_COURSE.getName()))
                .body("description", Matchers.equalTo(TEST_COURSE.getDescription()))
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
        Mockito.when(service.create(TEST_COURSE)).thenReturn(TEST_COURSE);

        RestAssured.given()
                .contentType("application/json")
                .body(TEST_COURSE)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("id", Matchers.is(TEST_ID))
                .body("name", Matchers.equalTo(TEST_COURSE.getName()))
                .body("description", Matchers.equalTo(TEST_COURSE.getDescription()))
                .statusCode(HttpStatus.CREATED.value());
    }

    @Test
    void testUpdate() {
        Mockito.doNothing().when(service).update(TEST_ID, UPDATED_COURSE);

        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COURSE)
                .put(url + "/" + TEST_ID)
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testUpdateNotFound() {
        Mockito.doThrow(TEST_EXCEPTION).when(service).update(TEST_ID, UPDATED_COURSE);

        RestAssured.given()
                .contentType("application/json")
                .body(UPDATED_COURSE)
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