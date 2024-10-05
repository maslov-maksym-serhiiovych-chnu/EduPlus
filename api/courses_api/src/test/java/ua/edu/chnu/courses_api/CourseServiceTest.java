package ua.edu.chnu.courses_api;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;
import ua.edu.chnu.courses_api.courses.Course;
import ua.edu.chnu.courses_api.courses.CourseNotFoundException;
import ua.edu.chnu.courses_api.courses.CourseRepository;
import ua.edu.chnu.courses_api.courses.CourseService;

import java.util.List;
import java.util.Optional;

@SpringBootTest
class CourseServiceTest {
    public static final int TEST_ID = 1;
    public static final Course TEST_COURSE = new Course(TEST_ID, "test", "test"),
            UPDATED_COURSE = new Course(TEST_ID, "updated", "updated");

    @Mock
    private CourseRepository repository;

    @InjectMocks
    private CourseService service;

    @Test
    void testCreate() {
        Mockito.when(repository.save(TEST_COURSE)).thenReturn(TEST_COURSE);

        Course created = service.create(TEST_COURSE);
        Assertions.assertEquals(TEST_COURSE, created);
    }

    @Test
    void testReadAll() {
        var courses = List.of(TEST_COURSE);
        Mockito.when(repository.findAll()).thenReturn(courses);

        var actualCourses = service.readAll();
        Assertions.assertEquals(List.of(TEST_COURSE), actualCourses);
    }

    @Test
    void testRead() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));

        Course course = service.read(TEST_ID);
        Assertions.assertEquals(TEST_COURSE, course);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundException.class, () -> service.read(TEST_ID));
    }

    @Test
    void testUpdate() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));
        Mockito.when(repository.save(TEST_COURSE)).thenReturn(TEST_COURSE);

        service.update(TEST_ID, UPDATED_COURSE);

        Course course = service.read(TEST_ID);
        Assertions.assertEquals(UPDATED_COURSE, course);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundException.class, () -> service.update(TEST_ID, UPDATED_COURSE));
    }

    @Test
    void testDelete() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));

        service.delete(TEST_ID);
        Mockito.verify(repository).delete(TEST_COURSE);
    }
    
    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());
        
        Assertions.assertThrows(CourseNotFoundException.class, () -> service.delete(TEST_ID));
    }
}