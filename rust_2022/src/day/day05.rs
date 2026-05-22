use std::collections::HashMap;
use regex::Regex;

pub fn part_1(input: String) -> String
{
    let re = Regex::new(r"^\D+(\d+)\D+(\d+)\D+(\d+)$").unwrap();
    let crates_instructions = input
        .split_once("\r\n\r\n")
        .map(|s| (s.0, s.1.trim()))
        .filter(|s| !(s.0.is_empty() || s.1.is_empty()))
        .unwrap();
    let mut crates_str = crates_instructions.0.lines();
    let instructions = crates_instructions.1.lines();

    let last_line = crates_str.next_back().unwrap();
    let mut crates: HashMap<usize, Vec<u8>> = HashMap::new();
    let mut idx_to_stack: HashMap<usize, u8> = HashMap::new();

    for (idx, byte) in last_line.bytes().enumerate() {
        if byte == b' ' { continue }
        crates.insert( (byte - b'0') as usize, Vec::new());
        idx_to_stack.insert(idx, byte - b'0');
    }

    for line in crates_str {
        for (idx, byte) in line.bytes().enumerate() {
            if byte.is_ascii_alphabetic()
            {
                let stack_num = *idx_to_stack.get(&idx).unwrap() as usize;
                crates.get_mut(&stack_num).unwrap().insert(0, byte);
            }
        }
    }

    for instruction in instructions {
        if let Some(caps) = re.captures(instruction) {
            let count: usize = caps[1].parse().unwrap();
            let from: usize = caps[2].parse().unwrap();
            let to: usize = caps[3].parse().unwrap();

            for _ in 0..count {
                let byte = crates.get_mut(&from).unwrap().pop().unwrap();
                crates.get_mut(&to).unwrap().push(byte);
            }
        }
    }

    let mut result = vec![0u8; crates.len()];
    for stack in crates {
        result[stack.0 - 1] = *stack.1.last().unwrap();
    }
    let result = String::from_utf8(result)
        .unwrap_or_else(|_| "Error".to_string());

    result
}

pub fn part_2(input: String) -> String
{
    let re = Regex::new(r"^\D+(\d+)\D+(\d+)\D+(\d+)$").unwrap();
    let crates_instructions = input
        .split_once("\r\n\r\n")
        .map(|s| (s.0, s.1.trim()))
        .filter(|s| !(s.0.is_empty() || s.1.is_empty()))
        .unwrap();
    let mut crates_str = crates_instructions.0.lines();
    let instructions = crates_instructions.1.lines();

    let last_line = crates_str.next_back().unwrap();
    let mut crates: HashMap<usize, Vec<u8>> = HashMap::new();
    let mut idx_to_stack: HashMap<usize, u8> = HashMap::new();

    for (idx, byte) in last_line.bytes().enumerate() {
        if byte == b' ' { continue }
        crates.insert( (byte - b'0') as usize, Vec::new());
        idx_to_stack.insert(idx, byte - b'0');
    }

    for line in crates_str {
        for (idx, byte) in line.bytes().enumerate() {
            if byte.is_ascii_alphabetic()
            {
                let stack_num = *idx_to_stack.get(&idx).unwrap() as usize;
                crates.get_mut(&stack_num).unwrap().insert(0, byte);
            }
        }
    }

    for instruction in instructions {
        if let Some(caps) = re.captures(instruction) {
            let count: usize = caps[1].parse().unwrap();
            let from: usize = caps[2].parse().unwrap();
            let to: usize = caps[3].parse().unwrap();

            let vec = crates.get_mut(&from).unwrap();
            let mut chunk = vec.split_off(vec.len() - count);
            crates.get_mut(&to).unwrap().append(&mut chunk);
        }
    }

    let mut result = vec![0u8; crates.len()];
    for stack in crates {
        result[stack.0 - 1] = *stack.1.last().unwrap();
    }
    let result = String::from_utf8(result)
        .unwrap_or_else(|_| "Error".to_string());

    result
}



