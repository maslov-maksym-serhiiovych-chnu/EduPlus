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
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.services.CourseService;

import java.util.ArrayList;

@Import(TestcontainersConfiguration.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class CoursesApplicationTests {
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
    void testGetAll() {
        var courses = new ArrayList<Course>();

        Course course = createCourse("test", "test");
        courses.add(course);

        Mockito.when(service.getAll()).thenReturn(courses);

        RestAssured.get(url)
                .then()
                .body(Matchers.notNullValue())
                .body("size()", Matchers.is(courses.size()))
                .body("[0].name", Matchers.equalTo(course.getName()))
                .body("[0].description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testGet() {
        Course course = createCourse("test", "test");

        Mockito.when(service.get(1)).thenReturn(course);

        RestAssured.get(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testCreate() {
        Course course = createCourse("test", "test");

        Mockito.when(service.create(course)).thenReturn(course);

        RestAssured.given()
                .contentType("application/json")
                .body(course)
                .post(url)
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.CREATED.value())
                .log()
                .all();
    }

    @Test
    void testUpdate() {
        Course course = createCourse("updated", "updated");

        Mockito.when(service.update(1, course)).thenReturn(course);

        RestAssured.given()
                .contentType("application/json")
                .body(course)
                .put(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    @Test
    void testDelete() {
        Course course = createCourse("test", "test");

        Mockito.when(service.delete(1)).thenReturn(course);

        RestAssured.delete(url + "/1")
                .then()
                .body(Matchers.notNullValue())
                .body("name", Matchers.equalTo(course.getName()))
                .body("description", Matchers.equalTo(course.getDescription()))
                .statusCode(HttpStatus.OK.value())
                .log()
                .all();
    }

    private static Course createCourse(String name, String description) {
        Course course = new Course();
        course.setName(name);
        course.setDescription(description);

        return course;
    }
}
