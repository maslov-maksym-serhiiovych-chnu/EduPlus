package ua.edu.chnu.comments;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;
import ua.edu.chnu.comments.services.CommentService;

import java.util.List;
import java.util.Optional;

class CommentServiceTest {
    @Mock
    private CommentRepository repository;

    @InjectMocks
    private CommentService service;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testCreate() {
        Comment comment = new Comment("test", "test");
        Mockito.when(repository.save(comment)).thenReturn(comment);

        Comment created = service.create(comment);

        Assertions.assertEquals(comment, created);
    }

    @Test
    void testReadAll() {
        Comment comment = new Comment("test", "test");
        Mockito.when(repository.findAll()).thenReturn(List.of(comment));

        var comments = service.readAll();

        Assertions.assertEquals(1, comments.size());
        Assertions.assertEquals(comment, comments.getFirst());
    }

    @Test
    void testRead() {
        Comment comment = new Comment("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(comment));

        Comment read = service.read(1);

        Assertions.assertEquals(comment, read);
    }

    @Test
    void testReadNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundByIdException.class, () -> service.read(1));
    }

    @Test
    void testUpdate() {
        Comment comment = new Comment("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(comment));
        Mockito.when(repository.save(comment)).thenReturn(comment);

        Comment updated = new Comment("updated", "updated");
        service.update(1, updated);

        Assertions.assertEquals(updated, comment);
    }

    @Test
    void testUpdateNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Comment updated = new Comment("updated", "updated");
        Assertions.assertThrows(CommentNotFoundByIdException.class, () -> service.update(1, updated));
        Mockito.verify(repository, Mockito.times(0)).save(updated);
    }

    @Test
    void testDelete() {
        Comment comment = new Comment("test", "test");
        Mockito.when(repository.findById(1)).thenReturn(Optional.of(comment));

        service.delete(1);

        Mockito.verify(repository, Mockito.times(1)).delete(comment);
    }

    @Test
    void testDeleteNotFound() {
        Mockito.when(repository.findById(1)).thenReturn(Optional.empty());

        Assertions.assertThrows(CommentNotFoundByIdException.class, () -> service.delete(1));
        Mockito.verify(repository, Mockito.times(0)).delete(Mockito.any());
    }
}
