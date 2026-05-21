pub fn part_1(input: String) -> u64
{
    let split: Vec<&str> = input.split("\r\n\r\n")
        .map(|s| s.trim())
        .filter(|s| !s.is_empty())
        .collect();
    let mut sum: u64 = 0;
    for item in split
    {
        let mut intermediate: u64 = 0;
        let elf_cal_str: Vec<&str> = item.split("\r\n").collect();
        for cal in elf_cal_str
        {
            intermediate += cal.parse::<u64>().unwrap();
        }
        if intermediate > sum {sum = intermediate;}
    }
    sum
}

pub fn part_2(input: String) -> u64
{
    let split = input.split("\r\n\r\n")
        .map(|s| s.trim())
        .filter(|s| !s.is_empty());
    let mut top_3: [u64; 3] = [0;3];
    for item in split
    {
        let mut intermediate: u64 = 0;
        let elf_cal_str = item.split("\r\n");
        for cal in elf_cal_str
        {
            intermediate += cal.parse::<u64>().unwrap();
        }
        for idx in (0..3).rev()
        {
            if intermediate > top_3[idx]
            {
                match idx
                {
                    2 => {
                        top_3[0] = top_3[1];
                        top_3[1] = top_3[2];
                        top_3[2] = intermediate;
                    }
                    1 => {
                        top_3[0] = top_3[1];
                        top_3[1] = intermediate;
                    }
                    0 => {
                        top_3[0] = intermediate;
                    }
                    _ => (),
                }
                break;
            }
        }
    }
    top_3[0] + top_3[1] + top_3[2]
}