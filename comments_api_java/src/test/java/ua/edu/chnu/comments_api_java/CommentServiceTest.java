package ua.edu.chnu.comments_api_java;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import ua.edu.chnu.comments_api_java.comments.Comment;
import ua.edu.chnu.comments_api_java.comments.CommentNotFoundException;
import ua.edu.chnu.comments_api_java.comments.CommentRepository;
import ua.edu.chnu.comments_api_java.comments.CommentService;

import java.util.List;
import java.util.Optional;

class CommentServiceTest {
    private static final Comment TEST_COMMENT = Comment.builder()
            .author("test")
            .content("test")
            .build();

    private static final Comment UPDATED_COMMENT = Comment.builder()
            .author("updated")
            .content("updated")
            .build();

    @Mock
    private CommentRepository repository;

    @InjectMocks
    private CommentService service;

    @BeforeEach
    void setUp() throws Exception {
        MockitoAnnotations.openMocks(this).close();
    }

    @Test
    void testCreate() {
        Mockito.when(repository.save(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        Comment created = service.create(TEST_COMMENT);
        Assertions.assertEquals(TEST_COMMENT, created);
    }

    @Test
    void testReadAll() {
        Mockito.when(repository.findAll()).thenReturn(List.of(TEST_COMMENT));

        var comments = service.readAll();
        Assertions.assertEquals(1, comments.size());
        Assertions.assertEquals(TEST_COMMENT, comments.getFirst());
    }

    @Test
    void testRead() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(TEST_COMMENT));

        Comment read = service.read(1);
        Assertions.assertEquals(TEST_COMMENT, read);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.read(1));
    }

    @Test
    void testUpdate() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(TEST_COMMENT));
        Mockito.when(repository.save(TEST_COMMENT)).thenReturn(TEST_COMMENT);

        service.update(1, UPDATED_COMMENT);
        Assertions.assertEquals(UPDATED_COMMENT, TEST_COMMENT);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.update(1, UPDATED_COMMENT));
        Mockito.verify(repository, Mockito.times(0)).save(UPDATED_COMMENT);
    }

    @Test
    void testDelete() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(TEST_COMMENT));

        service.delete(1);
        Mockito.verify(repository, Mockito.times(1)).delete(TEST_COMMENT);
    }

    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundException.class, () -> service.delete(1));
        Mockito.verify(repository, Mockito.times(0)).delete(Mockito.any());
    }
}