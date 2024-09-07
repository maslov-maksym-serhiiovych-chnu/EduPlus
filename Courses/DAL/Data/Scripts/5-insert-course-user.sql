create or replace procedure insert_course_user(p_course_id integer)
    language sql
as
$$
insert into course_users (course_id)
values (p_course_id)
$$