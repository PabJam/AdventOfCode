use std::str::FromStr;

pub fn part_1(input: String) -> u64
{
    let lines = input
        .lines()
        .map(|s| s.trim())
        .filter(|s| !s.is_empty());
    let mut sum: u64 = 0;
    for line in lines {
        let sections = line.split_once(",").unwrap();

        let first_section = sections.0.split_once("-").unwrap();
        let first_section = (u32::from_str(first_section.0).unwrap(), u32::from_str(first_section.1).unwrap());
        let second_section = sections.1.split_once("-").unwrap();
        let second_section = (u32::from_str(second_section.0).unwrap(), u32::from_str(second_section.1).unwrap());

        if (first_section.1 - first_section.0)  >= (second_section.1 - second_section.0) {
            if (first_section.0 <= second_section.0) && (first_section.1 >= second_section.1) {
                sum += 1;
            }
        }
        else {
            if (first_section.0 >= second_section.0) && (first_section.1 <= second_section.1) {
                sum += 1;
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
    for line in lines {
        let sections = line.split_once(",").unwrap();

        let first_section = sections.0.split_once("-").unwrap();
        let first_section = (u32::from_str(first_section.0).unwrap(), u32::from_str(first_section.1).unwrap());
        let second_section = sections.1.split_once("-").unwrap();
        let second_section = (u32::from_str(second_section.0).unwrap(), u32::from_str(second_section.1).unwrap());

        if (first_section.0 >= second_section.0) && (first_section.0 <= second_section.1) {
            sum += 1;
        }
        else if (first_section.1 >= second_section.0) && (first_section.1 <= second_section.1)  {
            sum += 1;
        }
        else if (first_section.0 <= second_section.0) && (first_section.1 >= second_section.1) {
            sum += 1;
        }

    }
    sum
}

