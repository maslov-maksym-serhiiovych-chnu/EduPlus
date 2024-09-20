package ua.edu.chnu.comments.models;

import jakarta.persistence.*;
import lombok.*;

@Data
@Entity
@Table(name = "comments")
public class Comment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(nullable = false)
    private String author;

    @Column(nullable = false)
    private String content;
}