PRAGMA foreign_keys = ON; -- Enable foreign key constraints for SQLite

CREATE TABLE IF NOT EXISTS user (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    login TEXT NOT NULL,
    password TEXT NOT NULL,
    email TEXT NOT NULL,
    is_selected BOOLEAN NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS task (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    description TEXT NOT NULL,
    created_date DATETIME NOT NULL,
    finish_date DATETIME,
    is_selected BOOLEAN NOT NULL DEFAULT 0,
    is_done BOOLEAN NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS pomodoro (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    is_selected BOOLEAN NOT NULL DEFAULT 0,
    time_spent INTEGER NOT NULL,
    time DATETIME NOT NULL,
    task_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (task_id) REFERENCES task(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE
);
