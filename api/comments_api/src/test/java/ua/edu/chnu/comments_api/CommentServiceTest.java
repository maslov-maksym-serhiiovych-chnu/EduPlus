package ua.edu.chnu.comments_api;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;
import ua.edu.chnu.comments_api.comments.Comment;
import ua.edu.chnu.comments_api.comments.CommentNotFoundException;
import ua.edu.chnu.comments_api.comments.CommentRepository;
import ua.edu.chnu.comments_api.comments.CommentService;

import java.util.List;
import java.util.Optional;

@SpringBootTest
public class CommentServiceTest {
    public static final int TEST_ID = 1;
    public static final Comment TEST_COMMENT = new Comment(TEST_ID, "test", 1),
            UPDATED_COMMENT = new Comment(TEST_ID, "updated", 2);

    @Mock
    private CommentRepository repository;

    @InjectMocks
    private CommentService service;

    @Test
    void testCreate() {
        Mockito.when(repository.save(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        Comment created = service.create(TEST_COMMENT);
        Assertions.assertEquals(TEST_COMMENT, created);
    }

    @Test
    void testReadAll() {
        var courses = List.of(TEST_COMMENT);
        Mockito.when(repository.findAll()).thenReturn(courses);

        var actualComments = service.readAll();
        Assertions.assertEquals(List.of(TEST_COMMENT), actualComments);
    }

    @Test
    void testRead() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COMMENT));

        Comment course = service.read(TEST_ID);
        Assertions.assertEquals(TEST_COMMENT, course);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.read(TEST_ID));
    }

    @Test
    void testUpdate() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COMMENT));
        Mockito.when(repository.save(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        service.update(TEST_ID, UPDATED_COMMENT);

        Comment course = service.read(TEST_ID);
        Assertions.assertEquals(UPDATED_COMMENT, course);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.update(TEST_ID, UPDATED_COMMENT));
    }

    @Test
    void testDelete() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.of(TEST_COMMENT));

        service.delete(TEST_ID);
        Mockito.verify(repository).delete(TEST_COMMENT);
    }

    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(TEST_ID)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.delete(TEST_ID));
    }
}