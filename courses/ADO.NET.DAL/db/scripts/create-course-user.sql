create or replace procedure create_course_user(p_course_id integer)
    language plpgsql
as
$$
begin
    insert into course_users (course_id)
    values (p_course_id);
end;
$$;