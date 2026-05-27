use std::collections::HashMap;

struct Directory {
    pub name: String,
    pub path: String,
    pub children: Vec<String>,
}

impl Directory {
    fn get_size(&self, structure: &HashMap<String, FileSystemEntity>) -> u64 {
        let mut sum: u64 = 0;

        for child_path in &self.children {
            let path = format!("{}\\{child_path}", self.path);
            if let Some(child_entity) = structure.get(path.as_str()) {
                sum += child_entity.get_size(structure);
            }
        }
        sum
    }
}

struct File {
    pub name: String,
    pub path: String,
    pub file_size: u64,
}

enum FileSystemEntity  {
    File(File),
    Directory(Directory)
}

impl FileSystemEntity {
    pub fn get_size(&self, structure: &HashMap<String, FileSystemEntity>) -> u64 {
        match self {
            FileSystemEntity::File(f) => f.file_size,
            FileSystemEntity::Directory(d) => d.get_size(structure)
        }
    }

    pub fn get_path(&self) -> String {
        match self {
            FileSystemEntity::File(f) => f.path.clone(),
            FileSystemEntity::Directory(d) => d.path.clone()
        }
    }

    pub fn get_name(&self) -> String {
        match self {
            FileSystemEntity::File(f) => f.name.clone(),
            FileSystemEntity::Directory(d) => d.name.clone()
        }
    }

    fn as_directory(&self) -> Option<&Directory> {
        match self {
            FileSystemEntity::Directory(d) => Some(d),
            _ => None, // Returns None if it's a File
        }
    }

    fn as_directory_mut(&mut self) -> Option<&mut Directory> {
        match self {
            FileSystemEntity::Directory(d) => Some(d),
            _ => None, // Returns None if it's a File
        }
    }
}



pub fn part_1(input: String) -> u64
{
    let lines = input
        .lines()
        .map(|s| s.trim())
        .filter(|s| !s.is_empty());
    let mut current_path = String::from("");
    let mut structure: HashMap<String, FileSystemEntity> = HashMap::new();
    let root = FileSystemEntity::Directory(Directory {
        name: String::from("/"),
        path: String::from("/"),
        children: Vec::new(),
    });
    structure.insert(root.get_path(), root);
    for line in lines {
        if line.starts_with("$ cd")  {
            let path = match line.rfind(' ') {
                Some(idx) => &line[idx+1..],
                None => panic!("cd command is wrong"),
            };
            if path.eq("..") {
                current_path = match current_path.rfind('\\') {
                    Some(idx) => current_path[..idx].to_string(),
                    None => "/".to_string()
                }
            }
            else {
                if current_path.is_empty() {
                    current_path = path.to_string();
                }
                else {
                    current_path = format!("{current_path}\\{path}");
                }

            }
        }
        else if line.starts_with("$ ls") { continue; }
        else {
            let (size, name) = line.split_once(' ').unwrap();
            let path = format!("{current_path}\\{name}");
            if structure.contains_key(&path) { continue; }
            let current_dir = structure.get_mut(current_path.as_str()).unwrap().as_directory_mut().unwrap();
            current_dir.children.push(name.to_string());
            if size.starts_with("dir") {
                let dir = FileSystemEntity::Directory(Directory {
                    name: name.to_string(),
                    path: path.to_string(),
                    children: Vec::new(),
                });
                structure.insert(path, dir);
            }
            else {
                let size: u64 = size.parse().unwrap();
                let file = FileSystemEntity::File(File{
                    name: name.to_string(),
                    path: path.to_string(),
                    file_size : size
                });
                structure.insert(path, file);
            }
        }

    }

    let mut sum: u64 = 0;
    for (_, entity) in structure.iter() {
        if let Some(dir) = entity.as_directory() {
            let value = dir.get_size(&structure);
            if value <= 100000 { sum += value; }
        }
    }

    sum
}

pub fn part_2(input: String) -> u64
{
    let lines = input
        .lines()
        .map(|s| s.trim())
        .filter(|s| !s.is_empty());
    let mut current_path = String::from("");
    let mut structure: HashMap<String, FileSystemEntity> = HashMap::new();
    let root = FileSystemEntity::Directory(Directory {
        name: String::from("/"),
        path: String::from("/"),
        children: Vec::new(),
    });
    structure.insert(root.get_path(), root);
    for line in lines {
        if line.starts_with("$ cd")  {
            let path = match line.rfind(' ') {
                Some(idx) => &line[idx+1..],
                None => panic!("cd command is wrong"),
            };
            if path.eq("..") {
                current_path = match current_path.rfind('\\') {
                    Some(idx) => current_path[..idx].to_string(),
                    None => "/".to_string()
                }
            }
            else {
                if current_path.is_empty() {
                    current_path = path.to_string();
                }
                else {
                    current_path = format!("{current_path}\\{path}");
                }

            }
        }
        else if line.starts_with("$ ls") { continue; }
        else {
            let (size, name) = line.split_once(' ').unwrap();
            let path = format!("{current_path}\\{name}");
            if structure.contains_key(&path) { continue; }
            let current_dir = structure.get_mut(current_path.as_str()).unwrap().as_directory_mut().unwrap();
            current_dir.children.push(name.to_string());
            if size.starts_with("dir") {
                let dir = FileSystemEntity::Directory(Directory {
                    name: name.to_string(),
                    path: path.to_string(),
                    children: Vec::new(),
                });
                structure.insert(path, dir);
            }
            else {
                let size: u64 = size.parse().unwrap();
                let file = FileSystemEntity::File(File{
                    name: name.to_string(),
                    path: path.to_string(),
                    file_size : size
                });
                structure.insert(path, file);
            }
        }

    }

    let used_space = structure.get("/").unwrap().get_size(&structure);
    let unused_space = 70000000 - used_space;
    let needed_space = 30000000 - unused_space;
    let mut smallest = u64::MAX;
    for (_, entity) in structure.iter() {
        if let Some(dir) = entity.as_directory() {
            let value = dir.get_size(&structure);
            if value > needed_space && value < smallest { smallest = value; }
        }
    }
    smallest
}

