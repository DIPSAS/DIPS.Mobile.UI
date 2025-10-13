#load "Models/SlackMessage.csx"
#load "Models/SectionType.csx"

public class SlackMessageBuilder
{
    private Block m_currentBlock;

    private List<Block> blocks;

    public SlackMessageBuilder()
    {
        Reset();
    }

    private void Reset()
    {
        blocks = new List<Block>();
    }

    public SlackMessageBuilder AddDivider()
    {
        AddNewBlock();
        SetBlockSectionType(SectionType.Divider);
        return this;
    }

    public SlackMessageBuilder AddNewBlock()
    {
        m_currentBlock = new Block();

        blocks.Add(m_currentBlock);

        return this;
    }

    public SlackMessageBuilder SetBlockSectionType(SectionType blockType)
    {
        m_currentBlock.Type = blockType.ToString().ToLower();

        return this;
    }

    public SlackMessageBuilder SetBlockText(Func<BlockText> blockText)
    {
        m_currentBlock.Text = blockText?.Invoke();

        return this;
    }

    public SlackMessageBuilder SetField(Func<Field> fieldBlock)
    {
        if(m_currentBlock.Fields == null)
        {
            m_currentBlock.Fields = new List<Field>(){fieldBlock?.Invoke()};
        }else
        {
            m_currentBlock.Fields.Add(fieldBlock?.Invoke());
        }
        return this;
    }

    public SlackMessageBuilder SetBlockAccessory(Func<Accessory> accessory)
    {
        m_currentBlock.Accessory = accessory?.Invoke();

        return this;
    }

    public SlackMessage Build()
    {
        var slackMessage = new SlackMessage
        {
            Blocks = new List<Block>(blocks)
        };

        Reset();

        return slackMessage;
    }
    

}