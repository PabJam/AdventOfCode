use std::fs;
pub mod day;
pub use day::*;
const FILE_PATH : &str = "C:\\Users\\PJamin\\projects\\AdventOfCode\\rust_2022\\input.txt";

fn main() {
    println!("In file {FILE_PATH}");

    let input = fs::read_to_string(FILE_PATH)
        .expect("Should have been able to read the file");
    let result = day02::part_2(input);
    println!("{result}");
}
