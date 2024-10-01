package ua.edu.chnu.courses.models;

import jakarta.persistence.*;
import lombok.*;

@NoArgsConstructor
@EqualsAndHashCode
@Entity
@Table(name = "courses")
public class Course {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Getter
    @Setter
    @Column(nullable = false)
    private String name;

    @Getter
    @Setter
    private String description;

    public Course(String name, String description) {
        this.name = name;
        this.description = description;
    }
}