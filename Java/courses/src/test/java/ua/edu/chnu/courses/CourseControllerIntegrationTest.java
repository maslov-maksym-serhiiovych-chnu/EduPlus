package ua.edu.chnu.courses;

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
import ua.edu.chnu.courses.exceptions.CourseNotFoundByIdException;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.services.CourseService;

import java.util.ArrayList;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CourseControllerIntegrationTest {
    @LocalServerPort
    private int port;

    @MockBean
    private CourseService service;

    public String url;

    @PostConstruct
    void init() {
        url = "http://localhost:" + port + "/api/courses";
    }

    @Test
    void testReadAll() {
        var courses = new ArrayList<Course>();
        Course course = new Course("test", "test");
        courses.add(course);
        Mockito.when(service.readAll()).thenReturn(courses);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(courses.size()))
                .body("[0].name", Matchers.equalTo(course.getName()))
                .body("[0].description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }

    @Test
    void testRead() {
        Course course = new Course("test", "test");
        Mockito.when(service.read(1)).thenReturn(course);

        RestAssured.get(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value());
    }


    @Test
    void testReadNotFound() {
        int id = Integer.MAX_VALUE;
        Mockito.when(service.read(id)).thenThrow(new CourseNotFoundByIdException(id));

        RestAssured.get(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("course not found by id: " + id))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }

    @Test
    void testCreate() {
        Course course = new Course("test", "test");
        Mockito.when(service.create(course)).thenReturn(course);

        RestAssured.given()
                .contentType("application/json")
                .body(course)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.CREATED.value());
    }

    @Test
    void testUpdate() {
        Course course = new Course("updated", "updated");
        Mockito.doNothing().when(service).update(1, course);

        RestAssured.given()
                .contentType("application/json")
                .body(course)
                .put(url + "/1")
                .then()
                .statusCode(HttpStatus.NO_CONTENT.value());
    }

    @Test
    void testUpdateNotFound() {
        int id = Integer.MAX_VALUE;
        Course course = new Course("updated", "updated");
        Mockito.doThrow(new CourseNotFoundByIdException(id)).when(service).update(id, course);

        RestAssured.given()
                .contentType("application/json")
                .body(course)
                .put(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("course not found by id: " + id))
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
        Mockito.doThrow(new CourseNotFoundByIdException(id)).when(service).delete(id);

        RestAssured.delete(url + "/" + id)
                .then()
                .body(Matchers.notNullValue())
                .body(Matchers.equalTo("course not found by id: " + id))
                .statusCode(HttpStatus.NOT_FOUND.value());
    }
}
