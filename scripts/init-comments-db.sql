create table if not exists courses
(
    id serial primary key
);

create table if not exists comments
(
    id        serial primary key,
    author    varchar(255)                not null,
    content   text                        not null,
    course_id int references courses (id) not null
)