create or replace procedure update_course_user_by_id(p_id integer, p_course_id integer)
    language sql
as
$$
update course_users
set course_id = p_course_id
where id = p_id
$$