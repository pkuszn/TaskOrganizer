PRAGMA foreign_keys = ON; -- Enable foreign key constraints for SQLite

CREATE TABLE IF NOT EXISTS user (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    login TEXT NOT NULL,
    password TEXT NOT NULL,
    email TEXT NOT NULL,
    is_valid BOOLEAN NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS task (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_pomodoro_session INTEGER NULL,
    name TEXT NOT NULL,
    id_user INTEGER NOT NULL,        
	description TEXT NOT NULL,
	created_date DATETIME NOT NULL,
	finish_date DATETIME NULL,
    is_done BOOLEAN NOT NULL DEFAULT 0,
    id_task_category INTEGER NOT NULL,
	FOREIGN KEY (id_user) REFERENCES user(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS pomodoro (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_pomodoro_session INTEGER NOT NULL,
    interval INTEGER NOT NULL,
    created_date DATETIME NOT NULL,
    finish_date DATETIME NULL,
    id_task INTEGER NOT NULL,
    id_user INTEGER NOT NULL,
    FOREIGN KEY (id_task) REFERENCES task(id),
    FOREIGN KEY (id_user) REFERENCES user(id)
);

CREATE TABLE IF NOT EXISTS pomodoro_session(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_user INTEGER NOT NULL,
    created_date DATETIME NOT NULL,
    finish_date DATETIME NULL,
    pomodoro_interval INTEGER NOT NULL,
    short_break INTEGER NOT NULL,
    long_break INTEGER NOT NULL,
    auto_restart_break BOOLEAN NOT NULL,
    auto_restart_pomodoro BOOLEAN NOT NULL,
    long_brake_interval INTEGER NOT NULL,
    id_alarm_sound INTEGER NOT NULL,
    repeat BOOLEAN NOT NULL,
    FOREIGN KEY (id_user) REFERENCES user(id)
    FOREIGN KEY (id_alarm_sound) REFERENCES alarm(id)
);


CREATE TABLE IF NOT EXISTS alarm(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    path TEXT NOT NULL
);


CREATE TABLE IF NOT EXISTS pomodoro_interval(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    path TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS short_break(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE long_break(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE task_category(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);
