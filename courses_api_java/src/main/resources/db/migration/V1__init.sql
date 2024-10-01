CREATE TABLE courses
(
    id          SERIAL PRIMARY KEY,
    name        VARCHAR(255) NOT NULL,
    description TEXT
);

CREATE TABLE comments
(
    id        SERIAL PRIMARY KEY,
    author    VARCHAR(255) NOT NULL,
    content   TEXT         NOT NULL,
    course_id INT          NOT NULL references courses (id)
)