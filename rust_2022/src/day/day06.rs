
pub fn part_1(input: String) -> u64
{
    let lines = input.trim();
    let mut current_id: u32 = 0;
    for (idx, byte) in lines.bytes().enumerate() {

        current_id = current_id << 8 | u32::from(byte);
        let [a,b,c,d] = current_id.to_ne_bytes();
        if (a == b || a == c || a == d || b == c || b == d || c == d) == false && idx >= 3 {
            return (idx + 1) as u64;
        }
    }

    lines.len() as u64
}

pub fn part_2(input: String) -> u64
{
    let line = input.trim();
    let mut current_id: Vec<u8> = line.as_bytes()[..14].to_vec();
    for (idx, byte) in line.bytes().enumerate() {

        current_id.remove(0);
        current_id.push(byte);
        let mut found = false;
        'outer: for (key_idx, key) in current_id.iter().enumerate() {
            for (pair_idx, pair) in current_id.iter().enumerate() {
                if *key == *pair && key_idx != pair_idx {
                    found = true;
                    break 'outer;
                }
            }
        }
        if found == false
        {
            return (idx + 1) as u64;
        }
    }

    line.len() as u64
}