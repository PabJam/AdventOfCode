use std::collections::{HashMap, HashSet};

pub fn part_1(input: String) -> u64
{
    let lines = input
        .lines()
        .map(|s| s.trim())
        .filter(|s| !s.is_empty());
    let mut sum: u64 = 0;
    for line in lines {
        let mid = line.len() / 2;
        let (first, second) = (&line[..mid], &line[mid..]);
        let mut pouch_1 : HashMap<u8, u32> = HashMap::new();
        let mut pouch_2 : HashMap<u8, u32> = HashMap::new();
        for (item_1, item_2) in first.bytes().zip(second.bytes()) {
            let count_1 = pouch_1.entry(item_1).or_insert(0);
            *count_1 += 1;
            let count_2 = pouch_2.entry(item_2).or_insert(0);
            *count_2 += 1;
        }
        for key in pouch_1.keys()
        {
            if pouch_2.contains_key(&key) {
                if key.is_ascii_uppercase() {
                    sum += (*key - 'A' as u8 + 27) as u64;
                }
                else {
                    sum += (*key - 'a' as u8 + 1) as u64;
                }
                break;
            }
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
    let mut sum: u64 = 0;
    let mut rucksacks : Vec<HashSet<u8>> = Vec::new();
    for line in lines {
        rucksacks.push(HashSet::new());
        for item in line.bytes()
        {
            rucksacks.last_mut().unwrap().insert(item);
        }
    }
    for bags in rucksacks.chunks_exact(3) {
        for item in bags.first().unwrap() {
            if bags[1].contains(item) && bags[2].contains(item)
            {
                if item.is_ascii_uppercase() {
                    sum += (*item - 'A' as u8 + 27) as u64;
                }
                else {
                    sum += (*item - 'a' as u8 + 1) as u64;
                }
                break;
            }
        }
    }
    sum
}