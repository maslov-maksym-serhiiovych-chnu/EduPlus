package ua.edu.chnu.comments.models;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
@Table(name = "comments")
public class Comment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "author", nullable = false)
    private String author;

    @Column(name = "content", nullable = false, length = Integer.MAX_VALUE)
    private String content;
}