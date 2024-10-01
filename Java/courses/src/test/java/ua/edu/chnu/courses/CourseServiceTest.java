package ua.edu.chnu.courses;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import ua.edu.chnu.courses.exceptions.CourseNotFoundByIdException;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.repositories.CourseRepository;
import ua.edu.chnu.courses.services.CourseService;

import java.util.List;
import java.util.Optional;

class CourseServiceTest {
    @Mock
    private CourseRepository repository;

    @InjectMocks
    private CourseService service;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testCreate() {
        Course course = new Course("test", "test");
        Mockito.when(repository.save(course)).thenReturn(course);

        Course created = service.create(course);

        Assertions.assertEquals(course, created);
    }

    @Test
    void testReadAll() {
        Course course = new Course("test", "test");
        Mockito.when(repository.findAll()).thenReturn(List.of(course));

        var courses = service.readAll();

        Assertions.assertEquals(1, courses.size());
        Assertions.assertEquals(course, courses.getFirst());
    }

    @Test
    void testRead() {
        Course course = new Course("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(course));

        Course read = service.read(1);

        Assertions.assertEquals(course, read);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundByIdException.class, () -> service.read(1));
    }

    @Test
    void testUpdate() {
        Course course = new Course("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(course));
        Mockito.when(repository.save(course)).thenReturn(course);

        Course updated = new Course("updated", "updated");
        service.update(1, updated);

        Assertions.assertEquals(updated, course);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Course updated = new Course("updated", "updated");
        Assertions.assertThrows(CourseNotFoundByIdException.class, () -> service.update(1, updated));
        Mockito.verify(repository, Mockito.times(0)).save(updated);
    }

    @Test
    void testDelete() {
        Course course = new Course("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(course));

        service.delete(1);

        Mockito.verify(repository, Mockito.times(1)).delete(course);
    }

    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CourseNotFoundByIdException.class, () -> service.delete(1));
        Mockito.verify(repository, Mockito.times(0)).delete(Mockito.any());
    }
}
