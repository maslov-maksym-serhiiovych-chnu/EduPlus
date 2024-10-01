package ua.edu.chnu.courses_api_java;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import ua.edu.chnu.courses_api_java.courses.Course;
import ua.edu.chnu.courses_api_java.courses.CourseNotFoundException;
import ua.edu.chnu.courses_api_java.courses.CourseRepository;
import ua.edu.chnu.courses_api_java.courses.CourseService;

import java.util.List;
import java.util.Optional;

public class CourseServiceTest {
    private static final int TEST_ID = 1;

    private static final Course TEST_COURSE = new Course(TEST_ID, "test", "test"),
            UPDATED_COURSE = new Course(TEST_ID, "updated", "updated");

    @Mock
    private CourseRepository repository;

    @InjectMocks
    private CourseService service;

    @BeforeEach
    void setUp() throws Exception {
        MockitoAnnotations.openMocks(this).close();
    }

    @Test
    void testCreate() {
        Mockito.when(repository.save(Mockito.any())).thenReturn(TEST_COURSE);

        Course created = service.create(TEST_COURSE);
        Assertions.assertEquals(TEST_COURSE, created);
    }

    @Test
    void testReadAll() {
        Mockito.when(repository.findAll()).thenReturn(List.of(TEST_COURSE));

        var courses = service.readAll();
        Assertions.assertEquals(1, courses.size());
        Assertions.assertEquals(TEST_COURSE, courses.getFirst());
    }

    @Test
    void testRead() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));

        Course read = service.read(TEST_ID);
        Assertions.assertEquals(TEST_COURSE, read);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundException.class, () -> service.read(TEST_ID));
    }

    @Test
    void testUpdate() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));
        Mockito.when(repository.save(Mockito.any())).thenReturn(TEST_COURSE);

        service.update(TEST_ID, UPDATED_COURSE);
        Assertions.assertEquals(UPDATED_COURSE, TEST_COURSE);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundException.class, () -> service.update(TEST_ID, UPDATED_COURSE));
        Mockito.verify(repository, Mockito.times(0)).save(Mockito.any());
    }

    @Test
    void testDelete() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COURSE));

        service.delete(TEST_ID);
        Mockito.verify(repository, Mockito.times(1)).delete(TEST_COURSE);
    }

    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundException.class, () -> service.delete(TEST_ID));
        Mockito.verify(repository, Mockito.times(0)).delete(Mockito.any());
    }
}